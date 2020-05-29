import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

import { environment } from 'src/environments/environment';
import { IBasket } from '../shared/Models/basket';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl = environment.apiUrl;

  private basketSource = new BehaviorSubject<IBasket>(null);
  basket$ = this.basketSource.asObservable();

  constructor(private http: HttpClient) {}

  getBasket(id: string) {
    return this.http.get<IBasket>(`${this.baseUrl}/basket?id=${id}`)
      .pipe(
        map(basket => {
          this.basketSource.next(basket);
        })
      );
  }

  setBasket(basket: IBasket) {
    this.http.post<IBasket>(`${this.baseUrl}/basket`, basket)
        .subscribe(response => {
          this.basketSource.next(response);
        }, error => {
          console.log(error);
        });
  }

  getCurrentBasket() {
    return this.basketSource.value;
  }
}

import { Product } from './../shared/Models/Products';
import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

import { environment } from 'src/environments/environment';
import { IBasket, IBasketItem, Basket } from '../shared/Models/basket';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  private basketSource = new BehaviorSubject<IBasket>(null);

  baseUrl = environment.apiUrl;
  basket$ = this.basketSource.asObservable();
  readonly basketId = 'basket_id';

  constructor(private http: HttpClient) {}

  getBasket(id: string) {
    return this.http.get<IBasket>(`${this.baseUrl}/basket?id=${id}`)
      .pipe(
        map(basket => {
          this.basketSource.next(basket);
          console.log('BasketService.getBasket:');
          console.log(basket);
        })
      );
  }

  setBasket(basket: IBasket) {
    this.http.post<IBasket>(`${this.baseUrl}/basket`, basket)
        .subscribe(response => {
          this.basketSource.next(response);
          console.log('BasketService.setBasket:');
          console.log(response);
        }, error => {
          console.log(error);
        });
  }

  getCurrentBasket() {
    return this.basketSource.value;
  }

  addItemToBasket(product: Product, quantity: number) {
    const basketItemToAdd = this.mapProductToBasketItem(product, quantity);
    const basket = this.getCurrentBasket() ?? this.createBasket();
    basket.items = this.addOrUpdateItem(basket.items, basketItemToAdd);
    this.setBasket(basket);
  }

  private addOrUpdateItem(items: IBasketItem[], basketItemToAdd: IBasketItem): IBasketItem[] {
    const index = items.findIndex(item => item.id === basketItemToAdd.id);

    if (index === -1) {
      items.push(basketItemToAdd);
    }
    else {
      items[index].quantity += basketItemToAdd.quantity;
    }

    return items;
  }

  private createBasket(): IBasket {
    const basket = new Basket();
    localStorage.setItem(this.basketId, basket.id);
    return basket;
  }

  private mapProductToBasketItem(product: Product, quantity: number): IBasketItem {
    return {
      id: product.id,
      productName: product.name,
      price: product.price,
      quantity,
      pictureUrl: product.pictureUrl,
      brand: product.productBrand,
      type: product.productType
    };
  }
}

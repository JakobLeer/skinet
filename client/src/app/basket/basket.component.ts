import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

import { BasketService } from './basket.service';
import { IBasket } from '../shared/Models/basket';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent implements OnInit {
  basket$: Observable<IBasket>;

  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
  }

  removeBasketItem(id: number) {
    this.basketService.removeBasketItem(id);
  }

  decreaseQuantity(id: number) {
    this.basketService.decreaseQuantity(id);
  }

  increaseQuantity(id: number) {
    this.basketService.increaseQuantity(id);
  }
}

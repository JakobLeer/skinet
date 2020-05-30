import { Product } from './../shared/Models/Products';
import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

import { environment } from 'src/environments/environment';
import {
  IBasket,
  IBasketItem,
  Basket,
  BasketTotals,
} from '../shared/Models/basket';

@Injectable({
  providedIn: 'root',
})
export class BasketService {
  private basketSource = new BehaviorSubject<IBasket>(null);
  private basketTotalsSource = new BehaviorSubject<BasketTotals>(null);

  readonly basketId = 'basket_id';
  baseUrl = environment.apiUrl;

  basket$ = this.basketSource.asObservable();
  basketTotals$ = this.basketTotalsSource.asObservable();

  constructor(private http: HttpClient) {}

  getBasket(id: string) {
    return this.http.get<IBasket>(`${this.baseUrl}/basket?id=${id}`).pipe(
      map((basket) => {
        this.basketSource.next(basket);
        this.calculateTotals();
      })
    );
  }

  setBasket(basket: IBasket) {
    this.http.post<IBasket>(`${this.baseUrl}/basket`, basket).subscribe(
      (response) => {
        this.basketSource.next(response);
        this.calculateTotals();
      },
      (error) => {
        console.log(error);
      }
    );
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

  decreaseQuantity(id: number) {
    this.changeQuantity(id, -1);
  }

  increaseQuantity(id: number) {
    this.changeQuantity(id, 1);
  }

  changeQuantity(id: number, delta: number) {
    const basket = this.getCurrentBasket();
    const index = basket.items.findIndex((item) => item.id === id);

    if (index !== -1) {
      const basketItem = basket.items[index];

      if (basketItem.quantity + delta <= 0) {
        this.removeBasketItem(id);
      } else {
        basketItem.quantity = basketItem.quantity + delta;
      }
    }
    this.setBasket(basket);
  }

  removeBasketItem(id: number) {
    const basket = this.getCurrentBasket();
    const index = basket.items.findIndex((item) => item.id === id);

    if (index !== -1) {
      basket.items.splice(index, 1);
      this.setBasket(basket);
    }
  }

  private calculateTotals() {
    const basket = this.getCurrentBasket();
    const shipping = 0;
    const subtotal = basket.items.reduce(
      (sum, item) => sum + item.price * item.quantity,
      0
    );
    this.basketTotalsSource.next({
      shipping,
      subtotal,
      total: shipping + subtotal,
    });
  }

  private addOrUpdateItem(
    items: IBasketItem[],
    basketItemToAdd: IBasketItem
  ): IBasketItem[] {
    const index = items.findIndex((item) => item.id === basketItemToAdd.id);

    if (index === -1) {
      items.push(basketItemToAdd);
    } else {
      items[index].quantity += basketItemToAdd.quantity;
    }

    return items;
  }

  private createBasket(): IBasket {
    const basket = new Basket();
    localStorage.setItem(this.basketId, basket.id);
    return basket;
  }

  private mapProductToBasketItem(
    product: Product,
    quantity: number
  ): IBasketItem {
    return {
      id: product.id,
      productName: product.name,
      price: product.price,
      quantity,
      pictureUrl: product.pictureUrl,
      brand: product.productBrand,
      type: product.productType,
    };
  }
}

import { Component, OnInit } from '@angular/core';

import { AccountService } from './account/account.service';
import { BasketService } from './basket/basket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'SkiNetty';

  constructor(private basketService: BasketService,
              private accountService: AccountService) {}

  ngOnInit(): void {
    this.loadBasket();
    this.loadCurrentUser();
  }

  loadBasket() {
    const basketId = localStorage.getItem(this.basketService.basketId);
    if (basketId) {
      this.basketService.getBasket(basketId).subscribe();
    }
  }

  loadCurrentUser() {
    const token = localStorage.getItem('token');
    if (token) {
      this.accountService.loadCurrentUser(token).subscribe(
        () => {
          console.log('Loaded user');
        },
        error => {
          console.log(error);
        }
      );
    }
  }
}

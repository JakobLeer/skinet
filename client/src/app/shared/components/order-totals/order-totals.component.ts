import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

import { BasketService } from 'src/app/basket/basket.service';
import { BasketTotals } from 'src/app/shared/Models/basket';

@Component({
  selector: 'app-order-totals',
  templateUrl: './order-totals.component.html',
  styleUrls: ['./order-totals.component.scss']
})
export class OrderTotalsComponent implements OnInit {
  basketTotals$: Observable<BasketTotals>;

  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    this.basketTotals$ = this.basketService.basketTotals$;
  }
}

import { Component, OnInit } from '@angular/core';

import { Product } from '../shared/Models/Products';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  products: Product[];

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.shopService.getProducts()
        .subscribe((response) => {
          this.products = response.result;
        }, error => {
          console.log(error);
        });
  }

}

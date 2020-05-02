import { ProductType } from './../shared/Models/ProductType';
import { Component, OnInit } from '@angular/core';

import { Brand } from '../shared/Models/Brand';
import { Product } from '../shared/Models/Products';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  products: Product[];
  brands: Brand[];
  productTypes: ProductType[];

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getProductTypes();
  }

  private getProducts() {
    this.shopService.getProducts().subscribe(
      (response) => {
        this.products = response.result;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  private getBrands() {
    this.shopService.getBrands().subscribe(
      (response) => {
        this.brands = response;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  private getProductTypes() {
    this.shopService.getProductTypes().subscribe(
      (response) => {
        this.productTypes = response;
      },
      (error) => {
        console.log(error);
      }
    );
  }
}

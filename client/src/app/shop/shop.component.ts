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

  brandIdSelected = 0;
  typeIdSelected = 0;
  sortOptionSelected = 'NameAsc';

  sortOptions = [
    { name: 'Alphabetical', value: 'NameAsc' },
    { name: 'Alphabetical Reversed', value: 'NameDesc' },
    { name: 'Price: Low to High', value: 'PriceAsc' },
    { name: 'Price: High to Low', value: 'PriceDesc' }
  ];
    constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getProductTypes();
  }

  private getProducts() {
    this.shopService.getProducts(this.brandIdSelected, this.typeIdSelected, this.sortOptionSelected).subscribe(
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
        this.brands = [ {id: 0, name: 'All'}, ...response];
      },
      (error) => {
        console.log(error);
      }
    );
  }

  private getProductTypes() {
    this.shopService.getProductTypes().subscribe(
      (response) => {
        this.productTypes = [ {id: 0, name: 'All'}, ...response];
      },
      (error) => {
        console.log(error);
      }
    );
  }

  onBrandSelected(brandId: number) {
    this.brandIdSelected = brandId;
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    this.typeIdSelected = typeId;
    this.getProducts();
  }

  onSortSelected(sortOption: string) {
    this.sortOptionSelected = sortOption;
    this.getProducts();
  }
}

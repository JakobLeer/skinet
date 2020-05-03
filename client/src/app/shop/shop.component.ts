import { ProductType } from './../shared/Models/ProductType';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';

import { Brand } from '../shared/Models/Brand';
import { Pagination } from 'src/app/shared/Models/Pagination';
import { Product } from '../shared/Models/Products';
import { ShopParams } from './models/productsParams';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  @ViewChild('search', {static: true}) searchTerm: ElementRef;

  products: Product[];
  brands: Brand[];
  productTypes: ProductType[];
  shopParams = new ShopParams();
  pagination: Pagination;

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
    this.shopService.getProducts(this.shopParams).subscribe(
      (response) => {
        this.products = response.result;
        this.pagination = response;
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
    this.shopParams.brandId = brandId;
    this.shopParams.pageIndex = 1;
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    this.shopParams.typeId = typeId;
    this.shopParams.pageIndex = 1;
    this.getProducts();
  }

  onSortSelected(sortOption: string) {
    this.shopParams.sortOption = sortOption;
    this.getProducts();
  }

  onPageIndexChanged(pageIndex: number) {
    if (this.shopParams.pageIndex !== pageIndex) {
      this.shopParams.pageIndex = pageIndex;
      this.getProducts();
      }
  }

  onSearch() {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageIndex = 1;
    this.getProducts();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}

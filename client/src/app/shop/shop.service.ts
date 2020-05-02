import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Brand } from 'src/app/shared/Models/Brand';
import { Pagination } from 'src/app/shared/Models/Pagination';
import { ProductType } from 'src/app/shared/Models/ProductType';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api';

  constructor(private http: HttpClient) {}

  getProducts() {
    return this.http.get<Pagination>(`${this.baseUrl}/products?pageSize=50`);
  }

  getBrands() {
    return this.http.get<Brand[]>(`${this.baseUrl}/products/brands`);
  }

  getProductTypes() {
    return this.http.get<ProductType[]>(`${this.baseUrl}/products/types`);
  }
}

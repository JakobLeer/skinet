import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';

import { Brand } from 'src/app/shared/Models/Brand';
import { environment } from 'src/environments/environment';
import { Pagination } from 'src/app/shared/Models/Pagination';
import { Product } from '../shared/Models/Products';
import { ProductType } from 'src/app/shared/Models/ProductType';
import { ShopParams } from './models/productsParams';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getProducts(shopParams: ShopParams) {
    let params = new HttpParams();

    if (shopParams.brandId) {
      params = params.append('brandId', shopParams.brandId.toString());
    }

    if (shopParams.typeId) {
      params = params.append('typeId', shopParams.typeId.toString());
    }

    if (shopParams.search) {
      params = params.append('name', shopParams.search);
    }

    params = params.append('sort', shopParams.sortOption);

    params = params.append('pageIndex', shopParams.pageIndex.toString());
    params = params.append('pageSize', shopParams.pageSize.toString());

    return this.http
      .get<Pagination>(`${this.baseUrl}/products`, {
        observe: 'response',
        params,
      })
      .pipe(map((response) => response.body));
  }

  getProduct(id: number) {
    return this.http.get<Product>(`${this.baseUrl}/products/${id}`);
  }

  getBrands() {
    return this.http.get<Brand[]>(`${this.baseUrl}/products/brands`);
  }

  getProductTypes() {
    return this.http.get<ProductType[]>(`${this.baseUrl}/products/types`);
  }
}

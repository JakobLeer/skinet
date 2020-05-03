import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';

import { Brand } from 'src/app/shared/Models/Brand';
import { Pagination } from 'src/app/shared/Models/Pagination';
import { ShopParams } from './models/productsParams';
import { ProductType } from 'src/app/shared/Models/ProductType';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api';

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

  getBrands() {
    return this.http.get<Brand[]>(`${this.baseUrl}/products/brands`);
  }

  getProductTypes() {
    return this.http.get<ProductType[]>(`${this.baseUrl}/products/types`);
  }
}

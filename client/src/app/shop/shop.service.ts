import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';

import { Brand } from 'src/app/shared/Models/Brand';
import { Pagination } from 'src/app/shared/Models/Pagination';
import { ProductType } from 'src/app/shared/Models/ProductType';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api';

  constructor(private http: HttpClient) {}

  getProducts(brandId?: number, typeId?: number, sortOption?: string) {
    let params = new HttpParams();

    if (brandId) {
      params = params.append('brandId', brandId.toString());
    }

    if (typeId) {
      params = params.append('typeId', typeId.toString());
    }

    if (sortOption) {
      params = params.append('sort', sortOption);
    }

    return this.http.get<Pagination>(`${this.baseUrl}/products`, {observe: 'response', params})
            .pipe(
              map(response => response.body)
            );
  }

  getBrands() {
    return this.http.get<Brand[]>(`${this.baseUrl}/products/brands`);
  }

  getProductTypes() {
    return this.http.get<ProductType[]>(`${this.baseUrl}/products/types`);
  }
}

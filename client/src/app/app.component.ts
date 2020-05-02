import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Product } from './shared/Models/Products';
import { Pagination } from './shared/Models/Pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'SkiNetty';
  products: Product[];

  constructor(private http: HttpClient) {
  }

  ngOnInit(): void {
    this.http.get('https://localhost:5001/api/products?pageSize=50')
      .subscribe((response: Pagination) => {
        console.log(response);
        this.products = response.result;
      }, error => {
        console.log(error);
      });
  }
}

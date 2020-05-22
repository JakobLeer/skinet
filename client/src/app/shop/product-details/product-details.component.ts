import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { Component, OnInit, Input } from '@angular/core';

import { Product } from 'src/app/shared/Models/Products';
import { ShopService } from './../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: Product;

  constructor(
    private shopService: ShopService,
    private activatedRoute: ActivatedRoute,
    private breadcrumbService: BreadcrumbService
  ) {
    this.breadcrumbService.set('@productDetails', '');
  }

  ngOnInit(): void {
    const id = +this.activatedRoute.snapshot.paramMap.get('id');
    if (id) {
      this.shopService.getProduct(id)
            .subscribe(product => {
              this.product = product;
              this.breadcrumbService.set('@productDetails', product.name);
            },
            error => {
              console.log(error);
            });
    }
  }

}

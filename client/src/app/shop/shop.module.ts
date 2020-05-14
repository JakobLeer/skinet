import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { ProductDetailsComponent } from './product-details/product-details.component';
import { ProductItemComponent } from './product-item/product-item.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { ShopComponent } from './shop.component';

@NgModule({
  declarations: [
    ProductItemComponent,
    ShopComponent,
    ProductDetailsComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    SharedModule
  ],
  exports: [
    ShopComponent
  ]
})
export class ShopModule { }

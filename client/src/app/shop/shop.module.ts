import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ProductDetailsComponent } from './product-details/product-details.component';
import { ProductItemComponent } from './product-item/product-item.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { ShopComponent } from './shop.component';
import { ShopRoutingModule } from './shop-routing.module';

@NgModule({
  declarations: [
    ProductItemComponent,
    ShopComponent,
    ProductDetailsComponent
  ],
  imports: [
    CommonModule,
    ShopRoutingModule,
    SharedModule
  ]
})
export class ShopModule { }

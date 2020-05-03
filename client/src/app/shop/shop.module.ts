import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductItemComponent } from './product-item/product-item.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { ShopComponent } from './shop.component';

@NgModule({
  declarations: [
    ProductItemComponent,
    ShopComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [
    ShopComponent
  ]
})
export class ShopModule { }

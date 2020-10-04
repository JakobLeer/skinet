import { CarouselModule } from 'ngx-bootstrap/carousel';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ReactiveFormsModule } from '@angular/forms';

import { OrderTotalsComponent } from './components/order-totals/order-totals.component';
import { PaginatorComponent } from './paginator/paginator.component';
import { PaginationHeaderComponent } from './pagination-header/pagination-header.component';


@NgModule({
  declarations: [
    PaginationHeaderComponent,
    PaginatorComponent,
    OrderTotalsComponent
  ],
  imports: [
    CarouselModule.forRoot(),
    CommonModule,
    PaginationModule.forRoot(), // Has its own providers and need to load these.
    ReactiveFormsModule
  ],
  exports: [
    CarouselModule,
    PaginationModule,
    PaginationHeaderComponent,
    PaginatorComponent,
    OrderTotalsComponent,
    ReactiveFormsModule
  ]
})
export class SharedModule { }

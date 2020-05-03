import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PaginatorComponent } from './paginator/paginator.component';
import { PaginationHeaderComponent } from './pagination-header/pagination-header.component';


@NgModule({
  declarations: [
    PaginationHeaderComponent,
    PaginatorComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot() // Has its own providers and need to load these.
  ],
  exports: [
    PaginationModule,
    PaginationHeaderComponent,
    PaginatorComponent
  ]
})
export class SharedModule { }

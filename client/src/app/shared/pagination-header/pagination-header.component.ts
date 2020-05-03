import { Component, OnInit, Input } from '@angular/core';

import { Pagination } from 'src/app/shared/Models/Pagination';

@Component({
  selector: 'app-pagination-header',
  templateUrl: './pagination-header.component.html',
  styleUrls: ['./pagination-header.component.scss']
})
export class PaginationHeaderComponent implements OnInit {
  @Input() pagination: Pagination;

  constructor() { }

  ngOnInit(): void {
  }

}

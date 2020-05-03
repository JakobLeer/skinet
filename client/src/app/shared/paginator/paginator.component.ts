import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { Pagination } from 'src/app/shared/Models/Pagination';

@Component({
  selector: 'app-paginator',
  templateUrl: './paginator.component.html',
  styleUrls: ['./paginator.component.scss']
})
export class PaginatorComponent implements OnInit {
  @Input() pagination: Pagination;
  @Output() pageIndexChanged = new EventEmitter<number>();

  constructor() { }

  ngOnInit(): void {
  }

  onPageChanged(event: any) {
    this.pageIndexChanged.emit(event.page);
  }
}

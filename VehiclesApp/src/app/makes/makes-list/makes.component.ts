import { Component, OnInit, HostListener } from '@angular/core';
import { Make } from 'src/app/_models/make';
import { MakeService } from 'src/app/_services/make.service';
import { ActivatedRoute } from '@angular/router';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';

@Component({
  selector: 'app-makes',
  templateUrl: './makes.component.html',
  styleUrls: ['./makes.component.css']
})
export class MakesComponent implements OnInit {
  makes: Make[];
  pagination: Pagination;
  sortOrder: any = {};
  searchString: string;

  constructor(private makeService: MakeService, private route: ActivatedRoute) {
    route.params.subscribe(val => {
      this.ngOnInit();
    });
   }

  ngOnInit() {
    this.route.data.subscribe(data => {
      // tslint:disable-next-line: no-string-literal
      this.makes = data['makes'].result;
      // tslint:disable-next-line: no-string-literal
      this.pagination = data['makes'].pagination;
    });
  }

  loadMakes() {
    this.makeService.getMakes(this.pagination.currentPage, this.pagination.itemsPerPage, this.sortOrder, this.searchString).subscribe(
      (makes: PaginatedResult<Make[]>) => {
        this.makes = makes.result;
        this.pagination = makes.pagination;
      });
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadMakes();
  }

  sortBy(columnName: any) {
    this.sortOrder = columnName;
    this.loadMakes();
  }

  search() {
    this.loadMakes();
  }

}

import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { ModelService } from 'src/app/_services/model.service';
import { Model } from 'src/app/_models/Model';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';

@Component({
  selector: 'app-models',
  templateUrl: './models.component.html',
  styleUrls: ['./models.component.css']
})
export class ModelsComponent implements OnInit {
  models: Model[];
  makeId: number;
  pagination: Pagination;
  sortOrder: any = {};
  searchString: string;

  constructor(private modelService: ModelService, private route: ActivatedRoute) {
    route.params.subscribe(val => {
      this.ngOnInit();
    });
   }

  ngOnInit() {
    this.route.params.subscribe(params => {
      // tslint:disable-next-line: no-string-literal
      this.makeId = +params['id'];
    });
    this.route.data.subscribe(data => {
      // tslint:disable-next-line: no-string-literal
      this.models = data['models'].result;
      // tslint:disable-next-line: no-string-literal
      this.pagination = data['models'].pagination;
    });
  }

  loadModels() {
    this.modelService.getModels(this.makeId, this.pagination.currentPage, this.pagination.itemsPerPage,
      this.sortOrder, this.searchString).subscribe(
      (models: PaginatedResult<Model[]>) => {
        this.models = models.result;
        this.pagination = models.pagination;
      });
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadModels();
  }

  sortBy(columnName: any) {
    this.sortOrder = columnName;
    this.loadModels();
  }

  search() {
    this.loadModels();
  }

}

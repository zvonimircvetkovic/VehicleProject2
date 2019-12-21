import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { Model } from '../_models/Model';
import { ModelService } from '../_services/model.service';
import { PaginatedResult } from '../_models/pagination';

@Injectable()
export class ModelsListResolver implements Resolve<PaginatedResult<Model[]>> {

    constructor(private modelService: ModelService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<Model[]>> {
        // tslint:disable-next-line: no-string-literal
        return this.modelService.getModels(route.params['id']);
      }
  }

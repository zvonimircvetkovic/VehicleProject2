import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { Model } from '../_models/Model';
import { ModelService } from '../_services/model.service';

@Injectable()
export class ModelResolver implements Resolve<Model> {

    constructor(private modelService: ModelService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Model> {
        // tslint:disable-next-line: no-string-literal
        return this.modelService.getModel(route.params['id'], route.params['modelId']);
      }
  }

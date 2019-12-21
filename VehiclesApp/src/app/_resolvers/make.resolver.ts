import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { Make } from '../_models/make';
import { MakeService } from '../_services/make.service';

@Injectable()
export class MakeResolver implements Resolve<Make> {

    constructor(private makeService: MakeService, private router: Router) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Make> {
        // tslint:disable-next-line: no-string-literal
        return this.makeService.getMake(route.params['id']);
      }
  }

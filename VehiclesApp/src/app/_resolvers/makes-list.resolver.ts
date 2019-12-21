import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { Make } from '../_models/make';
import { MakeService } from '../_services/make.service';
import { PaginatedResult } from '../_models/pagination';

@Injectable()
export class MakesListResolver implements Resolve<PaginatedResult<Make[]>> {

    constructor(private makeService: MakeService, private router: Router) {}

    resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<Make[]>> {
        return this.makeService.getMakes();
      }
  }

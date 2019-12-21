import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Make } from '../_models/make';
import { PaginatedResult, Pagination } from '../_models/pagination';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MakeService {
  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

getMakes(page?, itemsPerPage?, sortOrder?, searchString?): Observable<PaginatedResult<Make[]>> {
  const paginatedResult: PaginatedResult<Make[]> = new PaginatedResult<Make[]>();

  let params = new HttpParams();

  if (page != null && itemsPerPage != null) {
    params = params.append('pageNumber', page);
    params = params.append('pageSize', itemsPerPage);
  }

  if (sortOrder != null) {
    params = params.append('sortOrder', sortOrder);
  }

  if (searchString != null) {
    params = params.append('searchString', searchString);
  }

  return this.http.get<Make[]>(this.baseUrl, {observe: 'response', params}).pipe(
    map(response => {
      paginatedResult.result = response.body;
      if (response.headers.get('Pagination') != null) {
        paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
      }
      return paginatedResult;
    })
  );
}

getMake(id): Observable<Make> {
  return this.http.get<Make>(this.baseUrl + '/' + id);
}

updateMake(id, make: Make) {
  return this.http.put(this.baseUrl + '/' + id, make);
}

deleteMake(id) {
  return this.http.delete(this.baseUrl + '/' + id);
}

createMake(make: Make) {
  return this.http.post(this.baseUrl, make);
}

}

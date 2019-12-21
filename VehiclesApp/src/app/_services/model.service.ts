import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Model } from '../_models/Model';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ModelService {
  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }


getModels(makeId, page?, itemsPerPage?, sortOrder?, searchString?): Observable<PaginatedResult<Model[]>> {
  const paginatedResult: PaginatedResult<Model[]> = new PaginatedResult<Model[]>();

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

  return this.http.get<Model[]>(this.baseUrl + '/' + makeId + '/models', {observe: 'response', params}).pipe(
    map(response => {
      paginatedResult.result = response.body;
      if (response.headers.get('Pagination') != null) {
        paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
      }
      return paginatedResult;
    })
  );
}

getModel(makeId, id): Observable<Model> {
  return this.http.get<Model>(this.baseUrl + '/' + makeId + '/models/' + id);
}

updateModel(makeId, id, model: Model) {
  return this.http.put(this.baseUrl + '/' + makeId + '/models/' + id, model);
}

deleteModel(makeId, id) {
  return this.http.delete(this.baseUrl + '/' + makeId + '/models/' + id);
}

createModel(makeId, model: Model) {
  return this.http.post(this.baseUrl + '/' + makeId + '/models', model);
}

}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Model } from '../_models/Model';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Make } from '../_models/make';

@Injectable({
  providedIn: 'root'
})
export class ModelService {
  make: Make;

  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }


getModels(makeId): Observable<Model[]> {
  return this.http.get<Model[]>(this.baseUrl + '/' + makeId + '/models');
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

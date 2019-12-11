import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Make } from '../_models/make';

@Injectable({
  providedIn: 'root'
})
export class MakeService {
  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

getMakes(): Observable<Make[]> {
  return this.http.get<Make[]>(this.baseUrl);
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

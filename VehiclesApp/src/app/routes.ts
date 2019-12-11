import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MakesComponent } from './makes/makes-list/makes.component';
import { ModelsComponent } from './models/models-list/models.component';
import { MakeEditComponent } from './makes/make-edit/make-edit.component';
import { MakeCreateComponent } from './makes/make-create/make-create.component';
import { MakeDeleteComponent } from './makes/make-delete/make-delete.component';
import { ModelCreateComponent } from './models/model-create/model-create.component';
import { ModelEditComponent } from './models/model-edit/model-edit.component';
import { ModelDeleteComponent } from './models/model-delete/model-delete.component';

export const appRoutes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'makes', component: MakesComponent },
  { path: 'makes/create', component: MakeCreateComponent },
  { path: 'makes/:id/edit', component: MakeEditComponent },
  { path: 'makes/:id/delete', component: MakeDeleteComponent },
  { path: 'makes/:id/models', component: ModelsComponent },
  { path: 'makes/:id/models/create', component: ModelCreateComponent },
  { path: 'makes/:id/models/:modelId/edit', component: ModelEditComponent },
  { path: 'makes/:id/models/:modelId/delete', component: ModelDeleteComponent },
  { path: '**', redirectTo: 'home', pathMatch: 'full' },
];

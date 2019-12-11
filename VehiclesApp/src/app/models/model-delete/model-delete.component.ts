import { Component, OnInit } from '@angular/core';
import { Model } from 'src/app/_models/Model';
import { ModelService } from 'src/app/_services/model.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-model-delete',
  templateUrl: './model-delete.component.html',
  styleUrls: ['./model-delete.component.css']
})
export class ModelDeleteComponent implements OnInit {
  model: Model;
  makeId: number;
  id: number;

  constructor(private modelService: ModelService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      // tslint:disable-next-line: no-string-literal
      this.makeId = +params['id'];
      // tslint:disable-next-line: no-string-literal
      this.id = +params['modelId'];
      this.loadModel(this.makeId, this.id);
    });
  }

  loadModel(makeId, id) {
    this.modelService.getModel(makeId, id).subscribe((model: Model) => {
      this.model = model;
    });
  }

  deleteModel() {
    this.modelService.deleteModel(this.model.makeId, this.model.id).subscribe();
    this.router.navigate(['/makes', this.makeId, 'models']);
  }

}

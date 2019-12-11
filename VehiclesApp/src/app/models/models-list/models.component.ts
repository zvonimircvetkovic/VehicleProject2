import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { ModelService } from 'src/app/_services/model.service';
import { Model } from 'src/app/_models/Model';

@Component({
  selector: 'app-models',
  templateUrl: './models.component.html',
  styleUrls: ['./models.component.css']
})
export class ModelsComponent implements OnInit {
  models: Model[];
  makeId: number;

  constructor(private modelService: ModelService, private route: ActivatedRoute) {
    route.params.subscribe(val => {
      this.ngOnInit();
    });
   }

  ngOnInit() {
    this.route.params.subscribe(params => {
      // tslint:disable-next-line: no-string-literal
      this.makeId = +params['id'];
      this.loadModels(this.makeId);
    });
  }

  loadModels(makeId) {
    this.modelService.getModels(makeId).subscribe((models: Model[]) => {
      this.models = models;
    });
  }

}

import { Component, OnInit } from '@angular/core';
import { ModelService } from 'src/app/_services/model.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Model } from 'src/app/_models/Model';

@Component({
  selector: 'app-model-create',
  templateUrl: './model-create.component.html',
  styleUrls: ['./model-create.component.css']
})
export class ModelCreateComponent implements OnInit {
  model: Model;
  makeId: number;

  constructor(private modelService: ModelService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      // tslint:disable-next-line: no-string-literal
      this.makeId = +params['id'];
      // tslint:disable-next-line: no-string-literal
    });
    this.model = {
      makeId: this.makeId,
      name: '',
      abrv: ''
    };
  }

  createModel() {
    this.modelService.createModel(this.makeId, this.model).subscribe();
    this.router.navigate(['/makes', this.makeId, 'models']);
  }

}

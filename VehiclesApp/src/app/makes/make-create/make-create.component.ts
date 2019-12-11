import { Component, OnInit } from '@angular/core';
import { Make } from 'src/app/_models/make';
import { MakeService } from 'src/app/_services/make.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-make-create',
  templateUrl: './make-create.component.html',
  styleUrls: ['./make-create.component.css']
})
export class MakeCreateComponent implements OnInit {
  make: Make;

  constructor(private makeService: MakeService, private router: Router) { }

  ngOnInit() {
    this.make = {
      name: '',
      abrv: ''
    };
  }

  createMake() {
    this.makeService.createMake(this.make).subscribe();
    this.router.navigate(['makes']);
  }

}

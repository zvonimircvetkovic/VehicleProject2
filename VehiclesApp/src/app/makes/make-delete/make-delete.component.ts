import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Make } from 'src/app/_models/make';
import { MakeService } from 'src/app/_services/make.service';

@Component({
  selector: 'app-make-delete',
  templateUrl: './make-delete.component.html',
  styleUrls: ['./make-delete.component.css']
})
export class MakeDeleteComponent implements OnInit {
  make: Make;
  id: number;

  constructor(private makeService: MakeService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      // tslint:disable-next-line: no-string-literal
      this.id = +params['id'];
      this.loadMake(this.id);
    });
  }

  loadMake(id) {
    this.makeService.getMake(id).subscribe((make: Make) => {
      this.make = make;
    });
  }

  deleteMake() {
    this.makeService.deleteMake(this.make.id).subscribe();
    this.router.navigate(['makes']);
  }

}

import { Component, OnInit, HostListener } from '@angular/core';
import { Make } from 'src/app/_models/make';
import { MakeService } from 'src/app/_services/make.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-makes',
  templateUrl: './makes.component.html',
  styleUrls: ['./makes.component.css']
})
export class MakesComponent implements OnInit {
  makes: Make[];

  constructor(private makeService: MakeService, private route: ActivatedRoute) {
    route.params.subscribe(val => {
      this.ngOnInit();
    });
   }

  ngOnInit() {
    this.loadMakes();
  }

  loadMakes() {
    this.makeService.getMakes().subscribe((makes: Make[]) => {
      this.makes = makes;
    });
  }
}

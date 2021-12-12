import { Component, OnInit } from '@angular/core';
import { ItemDTO } from '../../../models/ItemDTO';
import { ItemsService } from '../../../providers/items.service';

@Component({
  selector: 'app-items-list',
  templateUrl: './items-list.component.html',
  styleUrls: ['./items-list.component.scss']
})
export class ItemsListComponent implements OnInit {
  items: ItemDTO[] = [];

  constructor(private itemService: ItemsService) { }

  ngOnInit(): void {
    this.itemService.getAll().subscribe((x) => {
      this.items = x;
    });
  }

}

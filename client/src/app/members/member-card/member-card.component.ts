import { Component, Input, OnInit } from '@angular/core';
import { TradingUserDto } from 'src/app/_models/TradingUserDto';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css'],
})
export class MemberCardComponent implements OnInit {
  @Input() tradingUser!: TradingUserDto;
  constructor() {}
  ngOnInit(): void {}
}

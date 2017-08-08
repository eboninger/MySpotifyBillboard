import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'roundTwoDec'
})
export class RoundTwoDecPipe implements PipeTransform {

  transform(input: number): number {
    return Math.round((input * 100) / 100);
  }

}

import { AfterViewInit, Directive, ElementRef, OnInit } from '@angular/core';

@Directive({
    selector: '[appFocusOnShow]',
})
export class FocusOnShowDirective implements AfterViewInit {
    constructor(private el: ElementRef) {
        if (!el.nativeElement['focus']) {
            throw new Error('Element does not accept focus.');
        }
    }

    ngAfterViewInit(): void {
        const input: HTMLInputElement = this.el.nativeElement as HTMLInputElement;
        input.focus();
        input.select();
    }
}
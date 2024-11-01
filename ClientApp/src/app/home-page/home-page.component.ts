import { animate } from '@angular/animations';
import { isPlatformBrowser } from '@angular/common';
import { Component, Inject, NgZone, PLATFORM_ID, OnDestroy, ViewChild, ElementRef, HostListener, OnInit } from '@angular/core';
import { TweenMax } from 'gsap';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {
  @ViewChild('canvass', { static: true }) canvasRef!: ElementRef<HTMLCanvasElement>;
  canvas: any;
  private effect!: Effect;
  private ctx!: CanvasRenderingContext2D;
  constructor(private ngZone: NgZone, @Inject(PLATFORM_ID) private platformId: any) { }
  
  ngOnInit(): void {
    this.ngZone.runOutsideAngular(() => {
      if (isPlatformBrowser(this.platformId)) {
        this.initCanvas();
      }
    });
  }

  private initCanvas(): void {
    this.canvas = this.canvasRef.nativeElement;
    this.ctx = this.canvas.getContext('2d', { willReadFrequently: true }) as CanvasRenderingContext2D;

    this.setCanvasSize();
    this.effect = new Effect(this.ctx, this.canvas.width, this.canvas.height);

    this.effect.wrapText('Hi, I\'m Mohammed Abdul Siddiq');
    this.effect.render();

    this.animate();

    window.addEventListener('resize', () => {
      this.setCanvasSize();
      this.effect.resize(this.canvas.width, this.canvas.height);
      this.effect.wrapText('Hi, I\'m Mohammed Abdul Siddiq');
    });
  }

  private setCanvasSize(): void {
    this.canvas.width = window.innerWidth;
    this.canvas.height = window.innerHeight;
  }

  private animate(): void {
    const animation = () => {
      this.ctx.clearRect(0, 0, this.canvas.width, this.canvas.height);
      this.effect.render();
      requestAnimationFrame(animation);
    };
    animation();
  }
  onClickAboutMe(){
    var elem = document.getElementById("aboutMeSection");
    if (elem) {
      elem.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
  }
}

class Particle {
  private effect: Effect;
  private x: number;
  private y: number;
  private color: string;
  private originX: number;
  private originY: number;
  private size: number;
  private dx: number;
  private dy: number;
  private vx: number;
  private vy: number;
  private force: number;
  private angle: number;
  private distance: number;
  private friction: number;
  private ease: number;

  constructor(effect: Effect, x: number, y: number, color: string) {
    this.effect = effect;
    this.x = Math.random() * this.effect.canvasWidth;
    this.y = this.effect.canvasHeight;
    this.color = color;
    this.originX = x;
    this.originY = y;
    this.size = this.effect.gap;
    this.dx = 0;
    this.dy = 0;
    this.vx = 0;
    this.vy = 0;
    this.force = 0;
    this.angle = 0;
    this.distance = 0;
    this.friction = Math.random() * 0.6 + 0.15;
    this.ease = Math.random() * 0.1 + 0.005;
  }

  public draw(): void {
    this.effect.context.fillStyle = this.color;
    this.effect.context.fillRect(this.x, this.y, this.size, this.size);
  }

  public update(): void {
    this.dx = this.effect.mouse.x - this.x;
    this.dy = this.effect.mouse.y - this.y;
    this.distance = this.dx * this.dx + this.dy * this.dy;
    this.force = -this.effect.mouse.radius / this.distance;

    if (this.distance < this.effect.mouse.radius) {
      this.angle = Math.atan2(this.dy, this.dx);
      this.vx += this.force * Math.cos(this.angle);
      this.vy += this.force * Math.sin(this.angle);
    }
    this.x += (this.vx *= this.friction) + (this.originX - this.x) * this.ease;
    this.y += (this.vy *= this.friction) + (this.originY - this.y) * this.ease;
  }
}

class Effect {
  public context: CanvasRenderingContext2D;
  public canvasWidth: number;
  public canvasHeight: number;
  public textX: number;
  public textY: number;
  public fontSize: number;
  public maxTextWidth: number;
  public lineHeight: number;
  public particles: Particle[];
  public gap: number;
  public mouse: { radius: number; x: number; y: number; };

  constructor(context: CanvasRenderingContext2D, canvasWidth: number, canvasHeight: number) {
    this.context = context;
    this.canvasWidth = canvasWidth;
    this.canvasHeight = canvasHeight;
    this.textX = this.canvasWidth / 2;
    this.textY = this.canvasHeight / 2;
    this.fontSize = Math.min(this.canvasWidth * 0.1, 100);
    this.lineHeight = this.fontSize * 1.1;
    this.maxTextWidth = this.canvasWidth * 0.8;
    this.particles = [];
    this.gap = 3;
    this.mouse = { radius: 10000, x: 0, y: 0 };

    this.initMouseMoveListener();
  }

  private initMouseMoveListener(): void {
    window.addEventListener('mousemove', (e) => {
      this.mouse.x = e.x;
      this.mouse.y = e.y;
    });
  }

  public wrapText(text: string): void {
    const gradient = this.context.createLinearGradient(0, 0, this.canvasWidth, this.canvasHeight);
    gradient.addColorStop(0.3, 'magenta');
    gradient.addColorStop(0.5, 'orange');
    gradient.addColorStop(0.7, 'yellow');

    this.context.fillStyle = gradient;
    this.context.textAlign = 'center';
    this.context.textBaseline = 'middle';
    this.context.lineWidth = 3;
    this.context.strokeStyle = 'white';

    this.context.font = `${this.fontSize}px Helvetica`;

    const linesArray: string[] = [];
    const words = text.split(' ');
    let lineCounter = 0;
    let line = '';

    for (const word of words) {
      const testLine = `${line}${word} `;
      if (this.context.measureText(testLine).width > this.maxTextWidth) {
        line = `${word} `;
        lineCounter++;
      } else {
        line = testLine;
      }
      linesArray[lineCounter] = line;
    }

    const textHeight = this.lineHeight * lineCounter;
    this.textY = this.canvasHeight / 2 - textHeight / 2;

    linesArray.forEach((line, index) => {
      this.context.fillText(line, this.textX, this.textY + (index * this.lineHeight));
      this.context.strokeText(line, this.textX, this.textY + (index * this.lineHeight));
    });

    this.convertParticles();
  }

  private convertParticles(): void {
    this.particles = [];
    const pixels = this.context.getImageData(0, 0, this.canvasWidth, this.canvasHeight).data;
    this.context.clearRect(0, 0, this.canvasWidth, this.canvasHeight);

    for (let y = 0; y < this.canvasHeight; y += this.gap) {
      for (let x = 0; x < this.canvasWidth; x += this.gap) {
        const index = (y * this.canvasWidth + x) * 4;
        const alpha = pixels[index + 3];
        if (alpha > 0) {
          const red = pixels[index];
          const green = pixels[index + 1];
          const blue = pixels[index + 2];
          const color = `rgb(${red},${green},${blue})`;
          this.particles.push(new Particle(this, x, y, color));
        }
      }
    }
  }

  public render(): void {
    this.particles.forEach(particle => {
      particle.update();
      particle.draw();
    });
  }

  public resize(width: number, height: number): void {
    this.canvasWidth = width;
    this.canvasHeight = height;
    this.textX = this.canvasWidth / 2;
    this.textY = this.canvasHeight / 2;
    this.fontSize = Math.min(this.canvasWidth * 0.1, 100);
    this.lineHeight = this.fontSize * 1.1;
    this.maxTextWidth = this.canvasWidth * 0.8;
  }

}




// A sword stabbed into the floor: a white pop, a flat shock ring, and chips of light kicked up.
import { Particles, Bits, P, B, Rng, fit, sparkle, band, clamp, smooth, ease, hex } from "../../core/index.js";
import { STEEL, SPARK } from "./_lib.js";

export const meta = {
  name: "Floor slam",
  desc: "A downward stab hits the floor at the origin: one white frame, an elliptical shock ring, glowing cracks and light chips.",
  tags: ["impact", "slam", "sword", "ground", "holy", "hero", "one-shot"],
  size: [144, 88],
  origin: [72, 68],
  duration: 0.55,
  loop: false,
  fps: 20,
  bg: "dark",
  options: { palette: STEEL, sparks: SPARK, seed: 7 },
};

export function create(o) {
  const C = fit(o.palette, 7), Rg = new Rng(o.seed);
  const ps = new Particles({ rng: Rg, ground: () => 2, sparks: o.sparks });
  const bits = new Bits({ ground: () => 1 });
  let t = 0, hit = false;
  const cracks = Array.from({ length: 5 }, (_, i) => {
    const a = -2.4 + (i / 4) * 4.8 + Rg.range(-0.15, 0.15);
    const L = Rg.range(16, 34);
    return { x: Math.cos(a) * L, y: Math.sin(a) * L * 0.28 };
  });
  return {
    update(dt) {
      t += dt;
      if (!hit && t >= 0.05) {
        hit = true;
        for (let i = 0; i < 12; i++) {
          const a = Rg.range(-2.4, -0.7);
          const sp = Rg.range(30, 90);
          ps.spawn(P.SPARK, Rg.range(-4, 4), -2, Math.cos(a) * sp, Math.sin(a) * sp, Rg.range(0.12, 0.22));
        }
        for (let i = 0; i < 7; i++) {
          bits.add({
            kind: B.CHUNK, x: Rg.range(-6, 6), y: -2,
            vx: Rg.range(-70, 70), vy: Rg.range(-160, -60),
            a: Rg.range(0, 3), va: Rg.range(-6, 6), s: Rg.range(1.2, 2.2),
            c: C[4], g: 0.7, drag: 1.5, bounce: 0.2, life: Rg.range(0.28, 0.4),
          });
        }
      }
      ps.update(dt);
      bits.update(dt);
    },
    draw(p) {
      if (t >= 0.55) return;
      const fade = 1 - smooth(0.32, 0.5, t);
      if (t < 0.08) {
        p.disc(0, -2, 10 * (1 - t / 0.08), C[6], 0.9);
        sparkle(p, 0, -2, 11 * (1 - t / 0.08), C[6], C[4], 0.7);
      }
      const rad = 8 + 46 * ease.outQuart(clamp(t / 0.26));
      const rk = 1 - smooth(0.22, 0.46, t);
      if (rk > 0.15) band(p, 0, 0, rad, 0.3, 2.1, 1, C, () => true, t < 0.12);
      const ck = fade * (t > 0.05 ? 1 : 0);
      for (const c of cracks) {
        p.line(0, 0, c.x, c.y, C[0], ck);
        p.line(0, 0, c.x * 0.92, c.y * 0.92, t < 0.16 ? C[6] : C[4], ck * 0.9);
      }
      p.glow(0, -1, 22, C[4], 0.4 * fade);
      ps.draw(p);
      bits.draw(p);
    },
  };
}

void hex;

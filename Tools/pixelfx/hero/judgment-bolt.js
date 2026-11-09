// A short warning, then a pale bolt. The path is seeded so every frame is the same strike.
import { Particles, P, Rng, fit, sparkle, bolt, band, clamp, smooth, ease } from "../../core/index.js";
import { STEEL, SPARK } from "./_lib.js";

export const meta = {
  name: "Judgment bolt",
  desc: "A glint marks the floor, then a pale blue-white bolt drops on the origin, forks once, and kicks a ring of sparks.",
  tags: ["lightning", "bolt", "holy", "hero", "one-shot", "ground"],
  size: [72, 168],
  origin: [36, 156],
  duration: 0.55,
  loop: false,
  fps: 20,
  bg: "dark",
  options: { palette: STEEL, sparks: SPARK, seed: 5 },
};

function stroke(p, pts, w, c, a) {
  for (let i = 1; i < pts.length; i++) p.beam(pts[i - 1][0], pts[i - 1][1], pts[i][0], pts[i][1], w, c, a);
}

export function create(o) {
  const C = fit(o.palette, 7), Rg = new Rng(o.seed);
  const ps = new Particles({ rng: Rg, ground: () => 4, sparks: o.sparks });
  const path = bolt(Rg, Rg.range(-3, 3), -146, 0, -2, { jag: 0.2, depth: 4 });
  const forkAt = path[Math.floor(path.length * 0.55)];
  const fork = bolt(Rg, forkAt[0], forkAt[1], forkAt[0] + 8, forkAt[1] + 14, { jag: 0.2, depth: 2 });
  let t = 0, hit = false;
  return {
    update(dt) {
      t += dt;
      if (!hit && t >= 0.12) {
        hit = true;
        for (let i = 0; i < 10; i++) {
          const a = Rg.range(-2.5, -0.6), sp = Rg.range(24, 70);
          ps.spawn(P.SPARK, Rg.range(-3, 3), -2, Math.cos(a) * sp * 0.7, Math.sin(a) * sp, Rg.range(0.1, 0.2));
        }
      }
      ps.update(dt);
    },
    draw(p) {
      if (t >= 0.55) return;
      const warn = clamp(t / 0.1);
      if (t < 0.12) {
        sparkle(p, 0, -2, 6 * warn, C[6], C[3], 0.45);
        p.glow(0, -2, 10, C[4], 0.2 * warn);
        stroke(p, path, 1.2, C[3], 0.25 * warn);
        return;
      }
      const age = t - 0.12;
      const flick = 0.72 + 0.28 * Math.abs(Math.sin(age * 48));
      const fade = 1 - smooth(0.22, 0.4, age);
      if (fade > 0.08) {
        stroke(p, path, 6.5 * fade, C[1], 1);
        stroke(p, path, 3.1 * flick * fade, age < 0.05 ? C[6] : C[5], 1);
        if (fade > 0.35) stroke(p, path, 1.3, C[6], 1);
        stroke(p, fork, 2 * fade, C[4], 1);
        p.glow(0, -64, 14, C[4], 0.16 * fade);
      }
      const rad = 6 + 16 * ease.outQuart(clamp(age / 0.18));
      if (age < 0.3) band(p, 0, 0, rad, 0.32, 1.8, 1, C, () => true, age < 0.08);
      if (age < 0.1) sparkle(p, 0, -2, 9 * (1 - age / 0.1), C[6], C[4], 0.6);
      ps.draw(p);
    },
  };
}


// A held crescent for a flying sword-wave. The glint walks the lip; motes use cycle so the loop has no seam.
import { fit, sparkle, cycle, TAU } from "../../core/index.js";
import { STEEL, SPARK } from "./_lib.js";

export const meta = {
  name: "Crescent wave",
  desc: "A vertical steel crescent that keeps its shape: a gold glint runs the outer lip and square motes peel off the back. Loop it on a particle that flies right.",
  tags: ["projectile", "slash", "sword", "wave", "holy", "hero", "loop"],
  size: [96, 112],
  origin: [48, 56],
  duration: 0.6,
  loop: true,
  fps: 20,
  bg: "dark",
  options: { palette: STEEL, sparks: SPARK, radius: 32, seed: 4 },
};

const A0 = -1.15, A1 = 1.15;

export function draw(p, t, o) {
  const C = fit(o.palette, 7), R = o.radius, cx = -R * 0.42, cy = 0;
  const ph = (t / meta.duration) * TAU;
  const breathe = 0.92 + 0.08 * Math.sin(ph);
  const w = 7.2 * breathe;
  const body = (u) => w * Math.pow(Math.sin(Math.PI * u), 0.5);
  p.arc(cx, cy, R + 1, (u) => body(u) + 2.4, A0, A1, C[0], 1);
  p.arc(cx, cy, R, body, A0, A1, (u, v) => {
    const glint = Math.exp(-Math.pow((u - (0.15 + 0.7 * (0.5 + 0.5 * Math.sin(ph)))) * 6, 2));
    if (glint > 0.45 && v > 0.4) return C[6];
    if (v > 0.8) return C[5];
    if (v > 0.5) return C[4];
    if (v > 0.25) return C[3];
    return C[2];
  }, 1);
  const gu = 0.15 + 0.7 * (0.5 + 0.5 * Math.sin(ph));
  const ga = A0 + (A1 - A0) * gu;
  const gx = cx + Math.cos(ga) * (R - 1), gy = cy + Math.sin(ga) * (R - 1);
  sparkle(p, gx, gy, 5, C[6], C[3], 0.4);
  p.glow(gx, gy, 12, C[5], 0.28);

  cycle(t, meta.duration, 7, o.seed, 0.42, (age, life, r) => {
    const u = r.next();
    const a = A0 + (A1 - A0) * u;
    const back = 6 + age * 28;
    const x = cx + Math.cos(a) * (R - 2) - back;
    const y = cy + Math.sin(a) * (R - 2) + (age - 0.1) * 10 * (r.next() - 0.4);
    const k = 1 - age / life;
    if (k <= 0) return;
    p.rect(x, y, 2, 2, age < 0.08 ? C[6] : C[4], k);
  });
}

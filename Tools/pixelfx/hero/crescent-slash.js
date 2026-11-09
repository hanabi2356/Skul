// The outlined crescent, the one-frame pop and the shatter follow the slash recipe in
// src/fx/melee/sword-slash.js, which adapts Sprite Fusion's open-source slash
// (https://www.spritefusion.com/pixel-art-vfx/slash), MIT License, Copyright (c) 2026 Hugo Duprez.
import { Particles, P, Rng, fit, sparkle, clamp, ease } from "../../core/index.js";
import { STEEL, SPARK } from "./_lib.js";

export const meta = {
  name: "Crescent slash",
  desc: "A steel crescent snaps in on one frame, holds a white lip, then breaks into square motes. Faces right; rotate it for an up cut, a down cut or a dash.",
  tags: ["melee", "slash", "sword", "holy", "hero", "one-shot"],
  size: [128, 128],
  origin: [64, 64],
  duration: 0.55,
  loop: false,
  fps: 20,
  bg: "dark",
  options: { palette: STEEL, sparks: SPARK, radius: 34, seed: 3 },
};

const A0 = -1.2, A1 = 1.2;

function crescent(p, C, cx, cy, R, a0, a1, w, hot, fade) {
  if (a1 - a0 < 0.05 || w < 0.6 || fade <= 0.02) return;
  const ink = (u) => (w + 2.6) * Math.pow(Math.sin(Math.PI * u), 0.42);
  const body = (u) => w * Math.pow(Math.sin(Math.PI * Math.pow(u, 0.85)), 0.5);
  p.arc(cx, cy, R + 1.1, ink, a0, a1, C[0], fade);
  p.arc(cx, cy, R, body, a0, a1, (u, v) => {
    if (hot && v > 0.62) return C[6];
    if (v > 0.78) return C[5];
    if (v > 0.48) return C[4];
    if (v > 0.24) return C[3];
    return C[2];
  }, fade);
}

function point(cx, cy, R, a) {
  return [cx + Math.cos(a) * R, cy + Math.sin(a) * R];
}

export function create(o) {
  const C = fit(o.palette, 7), Rg = new Rng(o.seed);
  const ps = new Particles({ rng: Rg, ground: () => 400, sparks: o.sparks });
  const R = o.radius, cx = -R * 0.55, cy = 0;
  let t = 0, broke = false;
  return {
    update(dt) {
      t += dt;
      if (!broke && t >= 0.2) {
        broke = true;
        for (let i = 0; i < 16; i++) {
          const a = A0 + (A1 - A0) * (i / 15);
          const [x, y] = point(cx, cy, R - 2, a);
          const ox = Math.cos(a), oy = Math.sin(a);
          const sp = 24 + (i % 4) * 14;
          ps.spawn(i % 3 === 0 ? P.SPARK : P.MOTE, x, y, ox * sp, oy * sp * 0.65, 0.16 + (i % 5) * 0.03, i % 2 ? C[6] : C[4], 1);
        }
      }
      ps.update(dt);
    },
    draw(p) {
      if (t >= 0.55) return;
      // short arc, then the lip is there in full for a few frames, then it is gone and the motes carry it
      const head = ease.outQuad(clamp((t + 0.04) / 0.12));
      const a1 = A0 + (A1 - A0) * Math.max(0.34, head);
      if (t < 0.24) {
        crescent(p, C, cx, cy, R, A0, a1, 9, t < 0.12, 1);
        const [x, y] = point(cx, cy, R - 2, a1);
        sparkle(p, x, y, 6 * (1 - t / 0.24), C[6], C[3], 0.45);
        p.glow(x, y, 10, C[4], 0.22 * (1 - t / 0.24));
      }
      ps.draw(p);
    },
  };
}

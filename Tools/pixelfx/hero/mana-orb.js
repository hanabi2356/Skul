// A small mana sphere. Orbiting squares and the core pulse are functions of t, so the loop meets itself.
import { fit, sparkle, TAU } from "../../core/index.js";
import { STEEL } from "./_lib.js";

export const meta = {
  name: "Mana orb",
  desc: "A white-cored steel orb with two squares orbiting it and a slow pulse. Loop.",
  tags: ["orb", "mana", "holy", "hero", "loop"],
  size: [64, 64],
  origin: [32, 32],
  duration: 0.8,
  loop: true,
  fps: 15,
  bg: "dark",
  options: { palette: STEEL, seed: 1 },
};

export function draw(p, t, o) {
  const C = fit(o.palette, 7);
  const ph = (t / meta.duration) * TAU;
  const pulse = 0.85 + 0.15 * Math.sin(ph * 2);
  p.disc(0, 0, 9 * pulse, C[3], 0.35);
  p.circle(0, 0, 6.5 * pulse, (d, ang) => {
    const lit = Math.cos(ang + 0.8) * 0.5 + 0.5;
    if (d > 0.9) return C[1];
    if (d < 0.34) return C[6];
    if (d < 0.62) return lit > 0.6 ? C[6] : C[5];
    return lit > 0.55 ? C[4] : C[3];
  }, 1);
  p.ring(0, 0, 11 * pulse, 1.4, (v) => (v > 0.6 ? C[5] : C[3]), 0.9);
  for (let i = 0; i < 2; i++) {
    const a = ph + i * Math.PI;
    const x = Math.cos(a) * 16, y = Math.sin(a) * 11;
    p.rect(x - 1.5, y - 1.5, 3, 3, i === 0 ? C[6] : C[4], 1);
    p.glow(x, y, 6, C[4], 0.2);
  }
  if (Math.sin(ph) > 0.65) sparkle(p, 4, -5, 4, C[6], C[3], 0.35);
  p.glow(0, 0, 16, C[4], 0.32 * pulse);
}

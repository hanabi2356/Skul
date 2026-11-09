// One puff of the violet mana smoke that telegraphs the sword-waves. Emitted as particles, so this loop is a single puff.
import { fit, TAU } from "../../core/index.js";
import { SMOKE } from "./_lib.js";

export const meta = {
  name: "Mana smoke",
  desc: "A single violet mana puff that swells, lifts and thins. Loop, meant to be emitted as particles.",
  tags: ["smoke", "mana", "telegraph", "hero", "loop"],
  size: [48, 48],
  origin: [24, 40],
  duration: 1,
  loop: true,
  fps: 12,
  bg: "dark",
  options: { palette: SMOKE, seed: 2 },
};

export function draw(p, t, o) {
  const C = fit(o.palette, 5);
  const ph = (t / meta.duration) * TAU;
  const lift = -3 + Math.sin(ph) * 2;
  p.disc(1, 2 + lift, 6.5, C[1], 0.4);
  p.disc(-3, lift, 4.2, C[2], 0.46);
  p.disc(4, -2 + lift * 0.3, 3.2, C[2], 0.32);
  p.disc(0, -4 + lift, 2.4, C[3], 0.28);
}

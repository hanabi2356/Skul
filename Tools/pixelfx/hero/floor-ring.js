import { fit, band } from "../../core/index.js";
import { STEEL } from "./_lib.js";

export const meta = {
  name: "Floor ring",
  desc: "A flat elliptical shock ring. A particle stamp; scale the particle to grow it.",
  tags: ["ring", "shock", "stamp", "hero"],
  size: [72, 28],
  origin: [36, 14],
  duration: 1,
  loop: true,
  fps: 1,
  bg: "dark",
  options: { palette: STEEL, seed: 1 },
};

export function draw(p, t, o) {
  const C = fit(o.palette, 7);
  band(p, 0, 0, 30, 0.34, 2.2, 1, C, () => true, true);
  void t;
}

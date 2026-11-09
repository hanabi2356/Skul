import { hex } from "../../core/index.js";

export const meta = {
  name: "Pixel spark",
  desc: "A 2px hot spark with a one-pixel tail. A particle stamp.",
  tags: ["spark", "stamp", "hero"],
  size: [16, 16],
  origin: [8, 8],
  duration: 1,
  loop: true,
  fps: 1,
  bg: "dark",
  options: { seed: 1 },
};

const W = hex("#ffffff"), G = hex("#ffe7a4"), C = hex("#7eaae6");

export function draw(p) {
  p.px(0, 0, W);
  p.px(1, 0, W);
  p.px(0, 1, G);
  p.px(-1, 0, G);
  p.px(-2, 0, C);
  p.px(-3, 1, C);
  p.glow(0, 0, 5, W, 0.35);
}

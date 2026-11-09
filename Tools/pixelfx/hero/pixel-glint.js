import { sparkle, hex } from "../../core/index.js";

export const meta = {
  name: "Pixel glint",
  desc: "A four-point glint. A particle stamp.",
  tags: ["glint", "stamp", "hero"],
  size: [16, 16],
  origin: [8, 8],
  duration: 1,
  loop: true,
  fps: 1,
  bg: "dark",
  options: { seed: 1 },
};

export function draw(p) {
  sparkle(p, 0, 0, 5, hex("#ffffff"), hex("#7eaae6"), 0.7);
}

// A long flat blade of sword-light. It shoots out from the origin and a pulse runs to the tip.
import { Particles, P, Rng, fit, sparkle, clamp, smooth, ease } from "../../core/index.js";
import { STEEL, SPARK } from "./_lib.js";

export const meta = {
  name: "Sword line",
  desc: "A straight sword-beam fires from the origin to the right: ink rim, white core, a pulse down the blade, sparks off the tip.",
  tags: ["projectile", "beam", "sword", "holy", "hero", "one-shot", "directional"],
  size: [240, 48],
  origin: [8, 24],
  duration: 0.55,
  loop: false,
  fps: 20,
  bg: "dark",
  options: { palette: STEEL, sparks: SPARK, reach: 200, seed: 2 },
};

export function create(o) {
  const C = fit(o.palette, 7), Rg = new Rng(o.seed);
  const ps = new Particles({ rng: Rg, ground: () => 400, sparks: o.sparks });
  const reach = o.reach;
  let t = 0, burst = false;
  return {
    update(dt) {
      t += dt;
      if (!burst && t >= 0.08) {
        burst = true;
        for (let i = 0; i < 10; i++) {
          const sp = Rg.range(40, 160);
          ps.spawn(P.SPARK, reach - 6, Rg.range(-4, 4), sp, Rg.range(-40, 40), Rg.range(0.12, 0.26));
        }
      }
      if (t > 0.1 && t < 0.42 && Rg.chance(0.55)) {
        const x = Rg.range(12, reach * clamp(t / 0.16));
        ps.spawn(P.MOTE, x, Rg.range(-3, 3), Rg.range(-20, 30), Rg.range(-16, 16), Rg.range(0.12, 0.22), C[5], 0.8);
      }
      ps.update(dt);
    },
    draw(p) {
      if (t >= 0.55) return;
      const grow = ease.outExpo(clamp((t + 0.045) / 0.12));
      const retract = smooth(0.32, 0.5, t);
      const x1 = 8 + (reach - 8) * grow * (1 - retract);
      const thick = 10 * (0.7 + 0.3 * grow);
      const pulse = clamp((t - 0.06) / 0.26);
      if (x1 > 14) {
        p.beam(2, 0, x1, 0, thick + 2.4, (u, v) => (v > 0.72 ? C[0] : C[1]), 1);
        p.beam(2, 0, x1, 0, thick, (u, v) => {
          const hot = Math.exp(-Math.pow((u - pulse) * 6, 2));
          const taper = 0.55 + 0.45 * (1 - u);
          if (v > taper) return C[1];
          if (hot > 0.45 && v < 0.65) return C[6];
          if (v < 0.2) return C[6];
          if (v < 0.48) return C[5];
          return C[3];
        }, 1);
      }
      if (t < 0.22 && x1 > 14) {
        sparkle(p, x1, 0, 6 * (1 - t / 0.22), C[6], C[4], 0.45);
        p.glow(x1, 0, 10, C[4], 0.26 * (1 - retract));
      }
      if (x1 > 20) p.glow(x1 * 0.55, 0, 12, C[3], 0.12 * (1 - retract));
      ps.draw(p);
    },
  };
}

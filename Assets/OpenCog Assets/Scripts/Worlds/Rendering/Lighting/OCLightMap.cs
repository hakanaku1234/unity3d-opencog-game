using UnityEngine;
using System.Collections;

namespace OpenCog.Map.Lighting
{
	public class OCLightMap {
	
		private Map3D<byte> lights = new Map3D<byte>();
		
		
		public bool SetMaxLight(byte light, Vector3i pos) {
			return SetMaxLight(light, pos.x, pos.y, pos.z);
		}
		public bool SetMaxLight(byte light, int x, int y, int z) {
			Vector3i chunkPos = OCChunk.ToChunkPosition(x, y, z);
			Vector3i localPos = OCChunk.ToLocalPosition(x, y, z);
			Chunk3D<byte> chunk = lights.GetChunkInstance(chunkPos);
			byte oldLight = chunk.Get(localPos);
			if(oldLight < light) {
				chunk.Set(light, localPos);
				return true;
			}
			return false;
		}
		
		public void SetLight(byte light, Vector3i pos) {
			SetLight(light, pos.x, pos.y, pos.z);
		}
		public void SetLight(byte light, int x, int y, int z) {
			lights.Set(light, x, y, z);
		}
		
		public byte GetLight(Vector3i pos) {
			return GetLight(pos.x, pos.y, pos.z);
		}
		public byte GetLight(int x, int y, int z) {
			byte light = lights.Get(x, y, z);
			if(light < OCLightComputer.MIN_LIGHT) return OCLightComputer.MIN_LIGHT;
			return light;
		}
		public byte GetLight(Vector3i chunkPos, Vector3i localPos) {
			byte light = lights.GetChunkInstance(chunkPos).Get(localPos);
			if(light < OCLightComputer.MIN_LIGHT) return OCLightComputer.MIN_LIGHT;
			return light;
		}
		
	}

}

// Based on https://www.scratchapixel.com/lessons/3d-basic-rendering/minimal-ray-tracer-rendering-simple-shapes/ray-sphere-intersection
void SphereRaycast_float(in float4 sphere, in float3 rayOrigin, in float3 rayDirection, 
	out float3 pos, out float3 normal, out float visible)
{
	float radius2 = sphere.w * sphere.w;

	float3 L = sphere.xyz - rayOrigin;
	float tca = dot(L, rayDirection);
	float d2 = dot(L, L) - (tca * tca);

	float thc = sqrt(radius2 - d2);
	visible = step(d2, radius2);

	float t = max(tca - thc, tca + thc);

	pos = rayOrigin + rayDirection * t;
	normal = normalize(pos - sphere.xyz);
}
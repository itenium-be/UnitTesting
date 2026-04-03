<template>
  <div class="flex justify-center items-center h-full gap-8 -mt-8">
    <!-- Left: Speed arrow -->
    <div class="flex h-96 gap-3 items-center">
      <div class="flex flex-col h-full py-2 justify-between">
        <div class="text-7xl -mt-8">🐢</div>
        <div class="text-7xl">🐇</div>
      </div>
      <svg width="24" height="100%" viewBox="0 0 24 380" class="h-96">
        <line x1="12" y1="18" x2="12" y2="362" stroke="#9ca3af" stroke-width="3"/>
        <polygon points="12,0 4,18 20,18" fill="#9ca3af"/>
        <polygon points="12,380 4,362 20,362" fill="#9ca3af"/>
      </svg>
    </div>

    <!-- Pyramid (SVG) -->
    <svg width="480" height="380" viewBox="0 0 480 380">
      <defs>
        <filter id="glow" x="-50%" y="-50%" width="200%" height="200%">
          <feGaussianBlur stdDeviation="6" result="coloredBlur">
            <animate attributeName="stdDeviation" values="6;20;6" dur="2s" repeatCount="indefinite"/>
          </feGaussianBlur>
          <feMerge>
            <feMergeNode in="coloredBlur"/>
            <feMergeNode in="SourceGraphic"/>
          </feMerge>
        </filter>
      </defs>
      <!-- E2E - top -->
      <path d="M240,15 L330,115 L150,115 Z" fill="#10b981"/>
      <text x="240" y="95" text-anchor="middle" fill="white" font-weight="bold" font-size="7">E2E</text>
      <!-- Integration - middle -->
      <path d="M150,115 L330,115 L400,235 L80,235 Z" fill="#06b6d4"/>
      <text x="240" y="182" text-anchor="middle" fill="white" font-weight="bold" font-size="8">Integration</text>
      <!-- Unit - bottom (with glow) -->
      <path d="M80,235 L400,235 L450,355 Q458,375 435,375 L45,375 Q22,375 30,355 Z" fill="#ec4899" filter="url(#glow)"/>
      <text x="240" y="325" text-anchor="middle" fill="white" font-weight="bold" font-size="9">Unit</text>
    </svg>

    <!-- Right: Cost arrow -->
    <div class="flex h-96 gap-3 items-center">
      <svg width="24" height="100%" viewBox="0 0 24 380" class="h-96">
        <line x1="12" y1="18" x2="12" y2="362" stroke="#9ca3af" stroke-width="3"/>
        <polygon points="12,0 4,18 20,18" fill="#9ca3af"/>
        <polygon points="12,380 4,362 20,362" fill="#9ca3af"/>
      </svg>
      <div class="flex flex-col h-full py-2 justify-between">
        <div class="text-4xl font-bold text-emerald-600 -mt-3">€€€</div>
        <div class="text-4xl font-bold text-emerald-600">$</div>
      </div>
    </div>
  </div>
</template>

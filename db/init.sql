CREATE TABLE
  IF NOT EXISTS links (
    id serial PRIMARY KEY,
    short_link text NOT NULL,
    long_url text NOT NULL,
    created_at timestamptz NOT NULL DEFAULT NOW (),
    updated_at timestamptz NOT NULL DEFAULT NOW ()
  );

INSERT INTO
  links (short_link, long_url)
VALUES
  (
    'https://short.link/2dRtlEl',
    'https://example.com/TerrificEarlyFlyingLizard'
  ),
  (
    'https://short.link/3CW0qQl',
    'https://example.com/PoorLoudTestingBuildHomework'
  ),
  (
    'https://short.link/jMsFAAY',
    'https://example.com/RainyColorfulProtectingMode'
  ),
  (
    'https://short.link/9fyLCEn',
    'https://example.com/RoundMilkySharingDevice'
  ),
  (
    'https://short.link/FzCjEzE',
    'https://example.com/SleepyRedundantGeneratingMonitor'
  ),
  (
    'https://short.link/3OZZU21',
    'https://example.com/PleasantApparentInstallingBread'
  ),
  (
    'https://short.link/x9TshCz',
    'https://example.com/YellowRoomyChattingImprovement'
  ),
  (
    'https://short.link/DDhzGaP',
    'https://example.com/AnimatedSuccessfulWaitingDisk'
  ),
  (
    'https://short.link/11hewQx',
    'https://example.com/UnwrittenRebelRestoringComparison'
  ),
  (
    'https://short.link/11hebXu',
    'https://example.com/RampantFeignedMissingSatisfaction'
  ),
  (
    'https://short.link/mVBRJiC',
    'https://example.com/PrimaryWirelessCopyingPixel'
  );